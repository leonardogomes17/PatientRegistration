import { Component, OnInit , Inject  } from '@angular/core';
import {MatButtonModule} from '@angular/material/button'
import {MatIconModule} from '@angular/material/icon'
import { MatFormFieldModule} from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input'
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog'

@Component({
  selector: 'app-dialog-box',
  imports: [MatButtonModule,MatIconModule,MatFormFieldModule,MatInputModule,MatDialogModule],
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.css']
})
export class DialogBoxComponent implements OnInit {

  public messageDialog!: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
   console.log('Parameter 1:', data.message);
   this.messageDialog = data.message;
  }

  ngOnInit() {
  }

}
