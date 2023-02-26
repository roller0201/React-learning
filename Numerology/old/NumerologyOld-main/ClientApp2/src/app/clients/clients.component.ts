import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { ClientModel } from 'src/models/client.model';
import { MatTableDataSource, MatPaginator, MatSort, MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from 'src/services/clientService';
import { FormControl } from '@angular/forms';
import * as moment from 'moment';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss']
})
export class ClientsComponent implements OnInit {
  clients: ClientModel[] = [];

  clientsMat = new MatTableDataSource<ClientModel>([]);
  displayedColumns:string[] = ['name' , 'surname', 'birthDate', 'telephone', 'email', 'skype', 'entryDate', 'actions'];
  dictionaryToAddOrUpdate: ClientModel = new ClientModel(0, "", "", "", true, "", "", "", "", "");
  isDisabledDelete: boolean = true;

  dialogResult: boolean = false;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  constructor(private toastr: ToastrService, private clientService: ClientService, public dialog: MatDialog) { }

  ngOnInit() {
    this.clientsMat.paginator = this.paginator;
    this.clientsMat.sort = this.sort;
  }

  ngAfterViewInit(){
    this.toastr.info('Ładowanie...', '', {
      positionClass: 'toast-bottom-right',     
    });

    this.refresh();
  }

  applyFilter(filterValue: string) {
    this.clientsMat.filter = filterValue.trim().toLowerCase();
  }

  onLoaded(clients: ClientModel[])
  {
    this.toastr.success('Pobrano', '', {
      positionClass: 'toast-bottom-right',     
    });

    this.clients = clients;
    this.clientsMat.data = this.convertToArray(this.clients); 
  }

  convertToArray(clients: any) : ClientModel[] {
    let output: ClientModel[] = [];
    for(let i = 0; i < clients.length; i++)
    {
      output.push(new ClientModel(clients[i].id, clients[i].name, clients[i].surname, clients[i].birthDate, clients[i].active, clients[i].telephone, clients[i].email, clients[i].skype, clients[i].entryDate, clients[i].note));
    }

    return output;
  }

  onError(data: any)
  {
    console.log(data);
    this.toastr.warning('Błąd', '', {
      positionClass: 'toast-bottom-right',     
    });
  }

  refresh()
  {
    this.clientService.getClients(this.paginator.pageIndex,this.paginator.pageSize).subscribe(result => this.onLoaded(result),
    error => this.onError(error));
  }

  edit(element: ClientModel)
  {
    this.openDialog(element);
  }

  add()
  {
    this.openDialog(new ClientModel(0, "", "", "", true, "", "", "", "", ""));
  }

  openDialog(element: ClientModel): void {
    const dialogRef = this.dialog.open(AddOrUpdateClientDialog, {
      width: '600px',
      data: element
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.dialogResult = result.success;
      if(this.dialogResult == true)
      {
        this.refresh();
      }
    });
  }
}


@Component({
  selector: 'client-dialog',
  templateUrl: 'client-dialog.html',
})
export class AddOrUpdateClientDialog {
  editMode: boolean = false;
  clientModel: ClientModel;
  date = new FormControl(new Date());

  constructor(
    public dialogRef: MatDialogRef<AddOrUpdateClientDialog>,
    @Inject(MAT_DIALOG_DATA) public client: ClientModel, private toastr: ToastrService, public datepipe: DatePipe, private clientService: ClientService) {
      this.clientModel = client;
      if(this.clientModel.id != 0)
      {
        this.editMode = true;
        this.date = new FormControl(new Date(this.clientModel.birthDate));
      }
    }

  save(): void {
    console.log(this.client.birthDate);
    this.client.birthDate = this.datepipe.transform(this.date.value, 'yyyy-MM-dd'); //moment(this.date.value, "YYYY-MM-DD")

    this.clientService.addOrUpdate(this.clientModel).subscribe(result => this.onSaveSuccess(result), 
    err => this.onSaveError(err))
    this.dialogRef.close();
  }

  onSaveSuccess(data: any): void {
    this.toastr.success('Pomyślnie zapisano', '', {
      positionClass: 'toast-bottom-right',     
    });
    this.dialogRef.close({ success: true });
  }

  onSaveError(data: any): void {
    this.toastr.warning('Błąd zapisu', '', {
      positionClass: 'toast-bottom-right',     
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  delete(): void {
    // Deactivate logic
  }

}
