import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MaterialConfig } from '../environments/material.import';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { SidebarComponent } from '../app/sidebar/sidebar.component';
import { HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { NameEndpoint } from '../endpoints/name.endpoint';
import { ConfigurationService } from '../services/configuration.service';
import { EndpointFactory } from '../environments/endpoint.factory';
import { PortraitComponent, ClientNameSelectPipe } from './portrait/portrait.component';
import { PortraitArchComponent } from './portrait-arch/portrait-arch.component';

import { NamesComponent } from './names/names.component';
import { LettersComponent } from './letters/letters.component';
import { ClientsComponent, AddOrUpdateClientDialog } from './clients/clients.component';
import { LettersService } from 'src/services/letterService';
import { LetterEndpoint } from 'src/endpoints/letter.endpoint';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule, ErrorStateMatcher, ShowOnDirtyErrorStateMatcher } from '@angular/material';
import { NameService } from 'src/services/nameService';
import { ClientService } from 'src/services/clientService';
import { ClientEndpoint } from 'src/endpoints/client.endpoint';
import { AddOrUpdateDictionaryRequest } from 'src/requests/addOrUpdateDictionaryRequest';
import { DatePipe } from '@angular/common';
import { NumerologyPortrait } from 'src/models/numerologyPortrait.model';
import { NumerologyService } from 'src/services/numerology.service';
import { NumerologyEndpoint } from 'src/endpoints/numerology.endpoint';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    PortraitComponent,
    PortraitArchComponent,
    NamesComponent,
    LettersComponent,
    ClientsComponent,
    AddOrUpdateClientDialog,
    ClientNameSelectPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialConfig,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    NgbModule,
    BsDropdownModule.forRoot(),
    PerfectScrollbarModule,
    HttpClientModule,
    FormsModule,
    HttpClientModule,
    MatNativeDateModule,
    ReactiveFormsModule,
  ],
  providers: [{
    provide: PERFECT_SCROLLBAR_CONFIG,
    useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
  },
  NameEndpoint,
  ConfigurationService,
  EndpointFactory,
  LettersService,
  LetterEndpoint,
  NameService,
  ClientEndpoint,
  ClientService,
  DatePipe,
  NumerologyService,
  NumerologyEndpoint,
  {provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher},
],
  entryComponents: [AddOrUpdateClientDialog],
  bootstrap: [AppComponent]
})
export class AppModule {

 }
