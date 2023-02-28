import { Component, OnInit, ViewChild } from '@angular/core';
import { NumerologyService } from 'src/services/numerology.service';
import { ClientService } from 'src/services/clientService';
import { ToastrService } from 'ngx-toastr';
import { ClientModel } from 'src/models/client.model';
import { NumerologyPortrait } from 'src/models/numerologyPortrait.model';
import { saveAs } from 'file-saver';
import { map, switchMap } from "rxjs/operators";
import { HttpEventType, HttpClient, HttpResponse } from '@angular/common/http';
import * as moment from 'moment';

@Component({
  selector: 'app-portrait',
  templateUrl: './portrait.component.html',
  styleUrls: ['./portrait.component.scss'],
  preserveWhitespaces: false 
})
export class PortraitComponent implements OnInit {
public clients: ClientModel[] = [];
public portraits: NumerologyPortrait[] = [];
public selectedClient: number = 0;
public canAddPortrait: boolean = false;
public isAdding: boolean = false;
public newPortraitBaseNames: string = "";
public editMode: boolean = false;
private objToEdit: NumerologyPortrait;
public objToEditBase: NumerologyNameModel = new NumerologyNameModel(null);
public objToEditAdded: NumerologyNameModel = new NumerologyNameModel(null);
public objtEditWhole: NumerologyNameModel = new NumerologyNameModel(null);
public addedNames: string = "";
public numbers: any = [];
public currentName: string = "";
public challenges: string[] = [];
public numerologyBirthDate: string = "";
public newPortraitDateBase: FormControl =  new FormControl(new Date());
public editId: number = 0;
panelOpenState = false;
portraitsMat = new MatTableDataSource<NumerologyPortrait>([]);
displayedColumns:string[] = ['id', 'baseNames' , 'birthDate', 'actions'];


@ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
@ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private numerologyService: NumerologyService, private clientService: ClientService, private sanitizer: DomSanitizer, private toastr: ToastrService, public datepipe: DatePipe, private http: HttpClient,private configuration: ConfigurationService,) {
    this.portraitsMat.paginator = this.paginator;
    this.portraitsMat.sort = this.sort;
   }

  onSelectedClientChange(newValue: number){
    this.newPortraitBaseNames = "";
    if(newValue != 0 && newValue != null)
    {
      this.selectedClient = newValue;
      this.canAddPortrait = true;
      this.loadClientPortraits();
    }
    else {
      this.canAddPortrait = false;
    }
  }

  loadClientPortraits() {
    this.isAdding = false;
    this.numerologyService.getClientPortraits(this.selectedClient).subscribe(result => this.onPortraitsLoaded(result), error => this.onPortraitsError(error));
  }

  loadClientDataToPortrait() {
    const selectedClientObject = this.clients.filter(x => x.id == this.selectedClient);
    this.newPortraitBaseNames = selectedClientObject[0].name + " " + selectedClientObject[0].surname;
    const splitDate = selectedClientObject[0].birthDate.split("-");
    this.newPortraitDateBase = new FormControl(new Date(Number(splitDate[0]), Number(splitDate[1]) -1, Number(splitDate[2])));
  }

  getShouldDisableSelect() {
    return this.isAdding || this.editMode;
  }

  edit(obj: NumerologyPortrait) {
    this.editMode = true;
    this.objToEdit = obj;
    this.isAdding = false;
    this.numerologyService.getNumerologyBirthDate(obj.birthDate).subscribe(res => {this.numerologyBirthDate = res.test;}, err => {console.log(err);});

    let names = obj.baseNames;
    this.numerologyService.getNumerologyName(names).subscribe(res => this.onObjToEditBaseLoaded(res), err => { console.log(err)});
    this.numerologyService.getNumerologyBirthDateTree(this.objToEdit.birthDate).subscribe(res => { this.challenges = res; }, err=> {console.log(err);});
    if(obj.addedNames != "")
    {
      this.addedNames = obj.addedNames;
      this.numerologyService.getNumerologyName(obj.addedNames).subscribe(res => this.onObjToEditAddedLoaded(res), err => { console.log(err)});
    }
    else
    {
      this.addedNames = "";
      //this.numerologyService.getNumerologyName(this.addedNames).subscribe(res => this.onObjToEditAddedLoaded(res), err => { console.log(err)});
    }
    this.numerologyService.getNumerologyNameWhole(names, obj.addedNames).subscribe(res => this.onObjToEditWholeLoaded(res), err => { console.log(err)});
  }

  getMainPersonNumber(): string {
    return this.numerologyBirthDate.split('/')[0];
  }

  onAddedNamesChanged()
  {
    if(this.addedNames != "")
    {
      this.numerologyService.getNumerologyName(this.addedNames).subscribe(res => this.onObjToEditAddedLoaded(res), err => { console.log(err)});
    }
    this.numerologyService.getNumerologyNameWhole(this.objToEdit.baseNames, this.addedNames).subscribe(res => this.onObjToEditWholeLoaded(res), err => { console.log(err)});
  }

  onObjToEditBaseLoaded(res: any)
  {
    debugger;
    this.objToEditBase = new NumerologyNameModel(res);
  }

  onObjToEditAddedLoaded(res: any)
  {
    this.objToEditAdded = new NumerologyNameModel(res);
  }

  onObjToEditWholeLoaded(res: any)
  {
    this.objtEditWhole = new NumerologyNameModel(res);
    let index = this.objToEdit.baseNames.length + 1 + this.addedNames.length;
    this.numbers = Array(index).fill(0).map((x,i)=>i);
    this.currentName = this.objToEdit.baseNames + " " + this.addedNames;
  }

  unEdit() {
    this.editMode = false;
    this.addedNames =  "";
    this.objToEditBase = new NumerologyNameModel(null);
    this.objToEditAdded = new NumerologyNameModel(null);
    this.objtEditWhole = new NumerologyNameModel(null);
  }

  savePortrait()
  {
    let objToSave = this.objToEdit;
    objToSave.addedNames = this.addedNames;

    this.numerologyService.addNewPortrait(objToSave).subscribe(res => this.onSavePortraitSuccess(res), err => {console.log(err)});
  }

  onSavePortraitSuccess(res: any)
  {
    this.toastr.success('Zapisano', '', {
      positionClass: 'toast-bottom-right',
      progressBar: true,
      
    });

    this.newPortraitBaseNames = "";
  }

  sumNumerologyNumbers(number: string, number2: string):string
  {
    let numberOne = +number;
    let numberTwo = +number2;
    let sum = numberOne + numberTwo;

    while(sum > 9)
    {
      sum -= 9;
    }

    return sum.toString();
  }

  sumNumbersFromPotentials(potential: NumerologyNameModel):string {
    let numberOne = +potential.one;
    let numberTwo = +potential.two;
    let numberThree = +potential.three;
    let numberFrour = +potential.four;
    let numberFive = +potential.five;
    let numberSix = +potential.six;
    let numberSeven = +potential.seven;
    let numberEight = +potential.eight;
    let numberNine = +potential.nine;
    let sum = numberOne + numberTwo + numberThree + numberFrour + numberFive + numberSix + numberSeven + numberEight + numberNine;

    return sum.toString();
  }

  sumNumbersNormal(number: string, number2: string): string {
    let numberOne = +number;
    let numberTwo = +number2;
    let sum = numberOne + numberTwo;

    return sum.toString();
  }

  substractionNumbersNormal(number: string, number2: string) : string {
    let numberOne = +number;
    let numberTwo = +number2;
    let substraction = numberOne - numberTwo;

    return substraction.toString();
  }

  delete(obj: any){
    this.numerologyService.deleteNumerologyPortrait(obj.id).subscribe(succ => {
      this.toastr.success("Pomyslnie usunieto");
      this.loadClientPortraits();
    }, err => {
      console.log(err);
      this.toastr.error("Blad przy usuwaniu", "Blad");
    })
  }

  editPortraitBase(obj: NumerologyPortrait) {
    this.isAdding = true;
    this.newPortraitBaseNames = obj.baseNames;
    const selectedClientObject = this.clients.filter(x => x.id == this.selectedClient);
    const splitDate = obj.birthDate.split("-");
    this.newPortraitDateBase = new FormControl(new Date(Number(splitDate[0]), Number(splitDate[1]) -1, Number(splitDate[2])));
    this.editId = obj.id;
  }

  addNewPortrait() {
    this.isAdding = true;
  }

  cancelNewPortrait() {
    this.isAdding = false;
    this.editId = 0;
  }

  saveNewPortrait() {
    if(this.newPortraitBaseNames == "")
      return;
    let id = 0;
    if(this.editId != 0)
    {
      id = this.editId;
      const old = this.portraits.filter(x => x.id == this.editId);
      const newPortraitToSave = new NumerologyPortrait(id, old[0].addedNames, "", this.selectedClient, "", this.newPortraitBaseNames, this.datepipe.transform(this.newPortraitDateBase.value, 'yyyy-MM-dd'));
      this.numerologyService.addNewPortrait(newPortraitToSave).subscribe(res => {
        this.loadClientPortraits();
        this.editId = 0;
        this.newPortraitBaseNames = "";
      }, err => this.onSaveNewPortraitError(err));
    }
    else{
      const newPortraitToSave = new NumerologyPortrait(0, "", "", this.selectedClient, "", this.newPortraitBaseNames, this.datepipe.transform(this.newPortraitDateBase.value, 'yyyy-MM-dd'));
      this.numerologyService.addNewPortrait(newPortraitToSave).subscribe(res => {
        this.loadClientPortraits();
        this.newPortraitBaseNames = "";
        this.editId = 0;
      }, err => this.onSaveNewPortraitError(err));
    }
  }

  print() {
    let req = {
      BaseNameModel: this.objToEditBase,
      AddedNameModel: this.objToEditAdded,
      WholeNameModel: this.objtEditWhole,
      PortraitId: this.objToEdit.id
    };

    this.numerologyService.printPortrait(req).subscribe(res => this.onPrintSuccess(res), err => {console.log(err);})
  }

  onPrintSuccess(res: any)
  {
    console.log('Res print: ' + res);
    let fileType = "application/pdf";
    debugger;
    /*this.numerologyService.getPrintPortrait(res.name).subscribe(res => {
      this.fetchImage('<host>/api/values/image');
    })*/
    let url = this.configuration.baseUrl + "/api/numerology/getPrint/" + res.name + "/";
    this.fetchImage(url);

    this.getPDF(res.name).subscribe(x => {
      debugger;
      // It is necessary to create a new blob object with mime-type explicitly set
      // otherwise only Chrome works like it should
      var newBlob = new Blob([x], { type: "application/pdf" });

      // IE doesn't allow using a blob object directly as link href
      // instead it is necessary to use msSaveOrOpenBlob
      if (window.navigator && window.navigator.msSaveOrOpenBlob) {
          window.navigator.msSaveOrOpenBlob(newBlob);
          return;
      }

      // For other browsers: 
      // Create a link pointing to the ObjectURL containing the blob.
      const data = window.URL.createObjectURL(newBlob);

      var link = document.createElement('a');
      link.href = data;
      link.download = res.name + ".pdf";
      // this is necessary as link.click() does not work on the latest firefox
      link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));

      setTimeout(function () {
          // For Firefox it is necessary to delay revoking the ObjectURL
          window.URL.revokeObjectURL(data);
          link.remove();
      }, 100);
  });
  }

  public getPDF(res: string): Observable<Blob> {   
    //const options = { responseType: 'blob' }; there is no use of this
    let url = this.configuration.baseUrl + "/api/numerology/getPrint/" + res + "/";
        // this.http refers to HttpClient. Note here that you cannot use the generic get<Blob> as it does not compile: instead you "choose" the appropriate API in this way.
        return this.http.get(url, { responseType: 'blob' });
    }

  private fetchImage(url: string): Observable<any> {
    return this.http
      .get(url, { responseType: 'blob' })
      .pipe(switchMap(blob => {
        return Observable.create((observer: Subscriber<SafeUrl>) => {
           const objectUrl = URL.createObjectURL(blob);
           const safe = this.sanitizer.bypassSecurityTrustUrl(objectUrl);
           observer.next(safe);
           return () => {
                if (objectUrl) {
                    URL.revokeObjectURL(objectUrl);
                }
            };
           });
         }));
     }


  onSaveNewPortraitError(err: any)
  {
    console.log(err);
  }

  onPortraitsLoaded(portraits: any) {
    this.portraits = this.convertToPortraitsArray(portraits);
    this.portraitsMat.data = this.portraits;
  }
  onPortraitsError(err: any)
  {
    console.log(err);
  }

  ngOnInit() {
    this.loadClients();

  }

  loadClients() {
    this.clientService.getAllClients().subscribe(result => this.onClientsLoaded(result),
    error=> this.onClientsError(error));
  }

  onClientsLoaded(clientsData: any[])
  {
    this.clients = [];
    this.clients = this.convertToArray(clientsData); 
  }

  convertToArray(clients: any) : ClientModel[] {
    let output: ClientModel[] = [];
    for(let i = 0; i < clients.length; i++)
    {
      output.push(new ClientModel(clients[i].id, clients[i].name, clients[i].surname, clients[i].birthDate, clients[i].active, clients[i].telephone, clients[i].email, clients[i].skype, clients[i].entryDate, clients[i].note));
    }

    return output;
  }

  convertToPortraitsArray(portraits: any) : NumerologyPortrait[] {
    let output: NumerologyPortrait[] = [];
    for(let i = 0; i < portraits.length; i++)
    {
      output.push(new NumerologyPortrait(portraits[i].id, portraits[i].addedNames, portraits[i].saveTime, portraits[i].clientId, portraits[i].note, portraits[i].baseNames, portraits[i].birthDate));
    }

    return output;
  }

  getIsValidNumberString(character: string): boolean {
    return character != "-"; 
  }


  onClientsError(err: any) {
    console.log(err);
  }

}


import { Pipe } from '@angular/core';
import { Observable, Subject, BehaviorSubject, Subscriber } from 'rxjs';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { FormControl } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { NumerologyNameModel } from 'src/models/numerologyName.model';
import { ConfigurationService } from 'src/services/configuration.service';
import { SafeUrl, DomSanitizer } from '@angular/platform-browser';

export interface PipeTransform {
  transform(value: any, ...args: any[]): any;
}

@Pipe({ name: 'clientNameSelect' })
export class ClientNameSelectPipe implements PipeTransform {
  transform(client: ClientModel)
  {
    return client.name + " " + client.surname + " " + client.telephone;
  }
}