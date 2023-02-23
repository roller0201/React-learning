import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PortraitComponent } from './portrait/portrait.component';
import { PortraitArchComponent } from './portrait-arch/portrait-arch.component';
import { NamesComponent } from './names/names.component';
import { LettersComponent } from './letters/letters.component';
import { ClientsComponent } from './clients/clients.component';


const routes: Routes = [
  { path: "", component: PortraitComponent, /*canActivate: [AuthGuard],*/ data: { title: "Home" } },
  { path: "portraitArch", component: PortraitArchComponent, /*canActivate: [AuthGuard],*/ data: { title: "PortreitArch" } },
  { path: "names", component: NamesComponent, /*canActivate: [AuthGuard],*/ data: { title: "Names" } },
  { path: "letters", component: LettersComponent, /*canActivate: [AuthGuard],*/ data: { title: "Letters" } },
  { path: "clients", component: ClientsComponent, /*canActivate: [AuthGuard],*/ data: { title: "Clients" } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
