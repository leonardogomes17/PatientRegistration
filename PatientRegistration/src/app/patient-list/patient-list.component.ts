import { Component, OnInit, Inject, inject } from '@angular/core';
import { Client, PatientDto } from '../patient-model-request/PatientRegistrationModel';
import { NgFor } from '@angular/common';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';


@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css'],
  imports: [NgFor, DatePipe, RouterModule ]
})
export class PatientListComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  public patientsList!: PatientDto[];
  private clientRequest!: Client;
  private dialogConfig!: MatDialogConfig;
  
  constructor() { 
    this.clientRequest = new Client();
    this.dialogConfig = new MatDialogConfig();
  }

  ngOnInit() {
    this.getAllPatient();
  }

  getAllPatient()
  {
    this.clientRequest.getAll().then(result => {
      this.patientsList = result;
    });
  }

  deletePatient(id: number | undefined)
  {
    this.clientRequest.desactive(id).then(result => {
      if(result)
      {
        
        this.sendMessage('Deactivated Successfully');       
        this.getAllPatient();
      }
      else
      {
        this.sendMessage('Deactivated Error!');
      }
    });
  }

   sendMessage(message: string)
   {
     this.dialogConfig.data = { message: message };
     this.dialog.open(DialogBoxComponent, this.dialogConfig);
   }

  alterPatient(id: number)
  {
    
  }

}
