import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { LetterEndpoint } from 'src/endpoints/letter.endpoint';
import { DictionaryModel } from 'src/models/dictionary.model';
import { BaseResponse } from 'src/models/base.response';
import { DeleteLetterRequest } from 'src/requests/deleteLetterRequest';
import { AddOrUpdateDictionaryRequest } from 'src/requests/addOrUpdateDictionaryRequest';

@Injectable()
export class LettersService {
  constructor(private http: HttpClient, private lettersEndpoint: LetterEndpoint)
  {

  }

  getAllLeters(){
    return this.lettersEndpoint.getLettersEndpoint<DictionaryModel[]>();
  }

  addOrUpdate(model: AddOrUpdateDictionaryRequest){
    return this.lettersEndpoint.getLettersAddOrupdateEndpoint<BaseResponse>(model);
  }

  deleteLetter(deleteRequest: DeleteLetterRequest){
    return this.lettersEndpoint.getLettersDeleteEndpoint<BaseResponse>(deleteRequest);
  }
}