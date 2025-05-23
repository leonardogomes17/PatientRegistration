import { Component, OnInit, Inject, inject } from '@angular/core';
import { AgreementDto, Client, PatientDto } from '../patient-model-request/PatientRegistrationModel';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { DatePipe, DOCUMENT, NgFor } from '@angular/common';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-patient-registration',
  templateUrl: './patient-registration.component.html',
  styleUrl: './patient-registration.component.css',
  imports: [NgFor, NgxMaskDirective, FormsModule],
})
export class PatientRegistrationComponent implements OnInit {
   
  public listAgreement!: AgreementDto[];
  readonly dialog = inject(MatDialog);
  public idPatient!: number;
  public patient!: PatientDto;
  private clientRequest!: Client;
  private dialogConfig!: MatDialogConfig;

  constructor(private route: ActivatedRoute, @Inject(DOCUMENT) document: Document) {
    this.clientRequest = new Client();
    this.dialogConfig = new MatDialogConfig();
  }

  

  ngOnInit(): void {
    this.getAgreements();
    this.route.queryParams.subscribe((queryParams: any) => {
      this.idPatient = queryParams['index'];
       if (this.idPatient > 0){
         this.clientRequest.getById(this.idPatient).then(result => {
           this.patient = result;
        });
      }
      else{
        this.clearModel();
      }
    });
  }

  onChangedBirthDate(value: any) {
    debugger;
    var domDateBirth = document.getElementById('DateBirth') as HTMLInputElement;
    if (value == null || value == undefined )
      domDateBirth.value =  '';
    else{
      try{
       domDateBirth.value = this.patient.dateBirth?.toLocaleDateString().substring(0,2) + '/' + this.patient.dateBirth?.toLocaleDateString().substring(3,5) + "/" + this.patient.dateBirth?.toLocaleDateString().substring(6,10);
      }catch {
        if (this.patient.cardValidate?.toString().length == 8)
          domDateBirth.value = this.patient.dateBirth?.toString().substring(0,2) + '/' + this.patient.dateBirth?.toString().substring(2,4) + "/" + this.patient.dateBirth?.toString().substring(5,9);
      }
    }
     //return '99/99/9999';
  }

  onChangedCardDate(value: any) {
    debugger;
    var domCardDate = document.getElementById('CardDate') as HTMLInputElement;
    if (value == null || value == undefined )
      domCardDate.value =  '';
    else{
      try{
        domCardDate.value = this.patient.cardValidate?.toLocaleDateString().substring(3,5) + '/' + this.patient.cardValidate?.toLocaleDateString().substring(6,10);
      }catch{
        if (this.patient.cardValidate?.toString().length == 7)
          domCardDate.value = this.patient.cardValidate?.toString().substring(2,4) + '/' + this.patient.cardValidate?.toString().substring(5,9);
        
      }
    }
  }

   getAgreements(){
        this.clientRequest.getAllAgreements().then(result => {
          this.listAgreement = result;
        });
   }

   saveOrUpdatePatient(){
  
        if (!this.isValidString(this.patient.cardNumber)){
          this.sendMessage("Card Number invalid!");
          return;   
        }

        if (!this.isValidString(this.patient.name)){
          this.sendMessage("Name is required");
          return;   
        }

        if (!this.isValidString(this.patient.surName)){
          this.sendMessage("SurName is required");
          return;   
        }

        if (!this.isValidString(this.patient.rg)){
          this.sendMessage("Rg is required");
          return;   
        }

        if (!this.isValidString(this.patient.cardNumber)){
          this.sendMessage("Card Number is required");
          return;   
        }

        if (!this.isValidString(this.patient.email)){
          this.sendMessage("Email is required");
          return;   
        }
        
        if (!this.isValidString(this.patient.ufRg) || this.patient.ufRg?.length != 2){
          this.sendMessage("UF is required");
          return;   
        }

        if (this.patient.agreementId != null && this.patient.agreementId <= 0){
          this.sendMessage("Agreement is required");
          return;   
        }

try {
  this.patient.dateBirth?.toLocaleDateString();
  }
  catch {
    if (this.patient.dateBirth != null ){
      this.patient.dateBirth = new Date( this.patient.dateBirth?.toString().substring(4,2) + "/" + this.patient.dateBirth?.toString().substring(0,2) + "/" + this.patient.dateBirth?.toString().substring(4,8));
    }
  }

  try {
      this.patient.cardValidate?.toLocaleDateString();
  }
  catch {
    if (this.patient.cardValidate != null){
      this.patient.cardValidate = new Date( this.patient.cardValidate?.toString().substring(0,2) + "/10" +  "/" + this.patient.cardValidate?.toString().substring(2,6));
    }
  }

    console.log(this.patient.dateBirth);
    console.log(this.patient);

    
        this.clientRequest.createOrEditPatient(this.patient).then(result => {
          if(result){
                  this.sendMessage('Saved Successfully');
                  if (this.patient.patientId  != null && this.patient.patientId <= 0) {
                    console.log('entrou');
                  }
          }
          else
              this.sendMessage('Saved Error!');
        }).catch( erro => {
          this.formatAndSendErrorApi(erro.toString());
          
        });
   }

   formatAndSendErrorApi(erro : string){
     var jsonReturn = JSON.parse('{' + erro.replace('Error','"Error"') + '}');
     this.sendMessage(jsonReturn.Error);
   }

   isValidString(value: string | undefined)
   {
     if (value == null || value == undefined || value == '')
      return false;
     else
      return true;
   }

   sendMessage(message: string)
   {
     this.dialogConfig.data = { message: message };
     this.dialog.open(DialogBoxComponent, this.dialogConfig);
   }

   clearModel(){
      this.patient = new PatientDto();
      this.patient.agreementId = 1;
      this.patient.gender = "M";
      this.patient.ufRg = "SP";
   }
}
