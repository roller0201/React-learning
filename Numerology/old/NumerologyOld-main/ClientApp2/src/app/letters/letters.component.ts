import { Component, OnInit, ViewChild } from '@angular/core';
import { DictionaryModel } from 'src/models/dictionary.model';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { LettersService } from 'src/services/letterService';

import {FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import { AddOrUpdateDictionaryRequest } from 'src/requests/addOrUpdateDictionaryRequest';
import { DeleteLetterRequest } from 'src/requests/deleteLetterRequest';
import { BaseModel } from 'src/models/base.model';


/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-letters',
  templateUrl: './letters.component.html',
  styleUrls: ['./letters.component.scss']
})
export class LettersComponent implements OnInit {
  letters: DictionaryModel[] = [];
  columns: any[] = [];
  loadingIndicator: boolean;
  isLoading: boolean = false;
  hasLetters: boolean = false;
  lettersMat = new MatTableDataSource<DictionaryModel>([]);
  displayedColumns:string[] = ['id', 'name' , 'value', 'actions', 'vowel'];
  dictionaryToAddOrUpdate: DictionaryModel = new DictionaryModel(0, "", "0");
  isDisabledDelete: boolean = true;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  letterFormControl = new FormControl('', [
    Validators.required,
    Validators.maxLength(1),
  ]);

  matcher = new MyErrorStateMatcher();

  constructor(private toastr: ToastrService, private letterService: LettersService) { }

  ngOnInit() {
    this.lettersMat.paginator = this.paginator;
    this.lettersMat.sort = this.sort;
  }

  ngAfterViewInit(){
    this.toastr.info('Ładowanie...', '', {
      positionClass: 'toast-bottom-right',     
    });

    this.refresh();
  }

  onLoaded(letters: DictionaryModel[])
  {
    this.toastr.success('Pobrano', '', {
      positionClass: 'toast-bottom-right',     
    });

    this.letters = letters;
    this.lettersMat.data = this.convertToArray(this.letters); 

    if(this.letters.length > 0)
    {
      this.hasLetters = true;
    }
  }

  convertToArray(letters: any) : DictionaryModel[] {
    let output: DictionaryModel[] = [];
    for(let i = 0; i < letters.length; i++)
    {
      output.push(new DictionaryModel(letters[i].id, letters[i].name, letters[i].value, letters[i].vowel))
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
    this.letterService.getAllLeters().subscribe(result => this.onLoaded(result),
    error => this.onError(error));
  }

  edit(data: DictionaryModel)
  {
    this.isDisabledDelete = false;
    this.dictionaryToAddOrUpdate = new DictionaryModel(data.id, data.name, data.value, data.vowel);
  }

  saveLetter()
  {
    console.log(this.dictionaryToAddOrUpdate);
    let arr: DictionaryModel[] = [];
    arr.push(this.dictionaryToAddOrUpdate);
    let request = new AddOrUpdateDictionaryRequest(arr);
    this.letterService.addOrUpdate(request).subscribe(success => this.onSaveSuccess(), err => this.onSaveError(err));
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
    this.letterService.deleteLetter(request).subscribe(success => this.onDeleteSuccess(), err => this.onDeleteError(err));
  }
  
  onDeleteSuccess()
  {
    this.isDisabledDelete = false;
    this.dictionaryToAddOrUpdate = new DictionaryModel(0, "", "0", false);
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
