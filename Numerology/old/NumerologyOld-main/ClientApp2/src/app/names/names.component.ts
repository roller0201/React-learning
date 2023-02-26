import { Component, OnInit, ViewChild } from '@angular/core';
import { NameService } from 'src/services/nameService';
import { ToastrService } from 'ngx-toastr';
import { DictionaryModel } from 'src/models/dictionary.model';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { BaseModel } from 'src/models/base.model';
import { DeleteLetterRequest } from 'src/requests/deleteLetterRequest';
import { AddOrUpdateDictionaryRequest } from 'src/requests/addOrUpdateDictionaryRequest';

@Component({
  selector: 'app-names',
  templateUrl: './names.component.html',
  styleUrls: ['./names.component.scss']
})
export class NamesComponent implements OnInit {
  names: DictionaryModel[] = [];
  columns: any[] = [];
  loadingIndicator: boolean;
  isLoading: boolean = false;
  namesMat = new MatTableDataSource<DictionaryModel>([]);
  displayedColumns:string[] = ['id', 'name' , 'actions'];
  dictionaryToAddOrUpdate: DictionaryModel = new DictionaryModel(0, "", "0");
  isDisabledDelete: boolean = true;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;


  constructor(private namesService: NameService, private toastr: ToastrService) { }

  ngOnInit() {
    this.namesMat.paginator = this.paginator;
    this.namesMat.sort = this.sort;
  }

  ngAfterViewInit(){
    this.toastr.info('Ładowanie...', '', {
      positionClass: 'toast-bottom-right',     
    });

    this.refresh();
  }

  onLoaded(names: DictionaryModel[])
  {
    this.toastr.success('Pobrano', '', {
      positionClass: 'toast-bottom-right',     
    });

    this.names = names;
    this.namesMat.data = this.convertToArray(this.names); 
  }

  convertToArray(names: any) : DictionaryModel[] {
    let output: DictionaryModel[] = [];
    for(let i = 0; i < names.length; i++)
    {
      output.push(new DictionaryModel(names[i].id, names[i].name, names[i].value))
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
    this.namesService.getAllNames().subscribe(result => this.onLoaded(result),
    error => this.onError(error));
  }

  edit(data: DictionaryModel)
  {
    this.isDisabledDelete = false;
    this.dictionaryToAddOrUpdate = new DictionaryModel(data.id, data.name, data.value);
  }

  saveLetter()
  {
    console.log(this.dictionaryToAddOrUpdate);
    let arr: DictionaryModel[] = [];
    arr.push(this.dictionaryToAddOrUpdate);
    let request = new AddOrUpdateDictionaryRequest(arr);
    this.namesService.addOrUpdate(request).subscribe(success => this.onSaveSuccess(), err => this.onSaveError(err));
  }

  onSaveSuccess()
  {
    this.toastr.success('Pomyślnie zapisano', '', {
      positionClass: 'toast-bottom-right',     
    });

    this.refresh();
  }

  onSaveError(data: any)
  {
    this.toastr.warning('Błąd', '', {
      positionClass: 'toast-bottom-right',     
    });
  }

  onKeydown(event) 
  {
    if(event.key === "Enter" && this.dictionaryToAddOrUpdate.name.length > 0)
    {
      this.saveLetter();
    }
  }
  
  cancelSaveLetter()
  {
    this.isDisabledDelete = true;
    this.dictionaryToAddOrUpdate = new DictionaryModel(0, "", "0");
  }

  deleteLetter(id: number)
  {
    let toDelete = new BaseModel(id);
    let out: BaseModel[] = [];
    out.push(toDelete);
    let request = new DeleteLetterRequest(out);
    this.namesService.deleteName(request).subscribe(success => this.onDeleteSuccess(), err => this.onDeleteError(err));
  }
  
  onDeleteSuccess()
  {
    this.isDisabledDelete = false;
    this.dictionaryToAddOrUpdate = new DictionaryModel(0, "", "0");
    this.toastr.success('Pomyślnie usunięto', '', {
      positionClass: 'toast-bottom-right',     
    });
    this.refresh();

  }

  onDeleteError(data: any)
  {
    this.toastr.warning('Błąd', '', {
      positionClass: 'toast-bottom-right',     
    });
  }

}
