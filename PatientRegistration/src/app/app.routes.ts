import { RouterModule, Routes } from '@angular/router';
import { PatientRegistrationComponent } from './patient-registration/patient-registration.component';
import { PatientListComponent } from './patient-list/patient-list.component';
import { NgModule } from '@angular/core';
// import { NavComponent } from './nav/nav.component';

export const routes: Routes = [
    { path: '', redirectTo: 'patientregistration', pathMatch: 'full' },
    { path: 'patientregistration', component: PatientRegistrationComponent},
    { path: 'patientlist', component: PatientListComponent}
    // { path: 'nav', component: NavComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)

    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
