import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { DictionaryModel } from 'src/models/dictionary.model';
import { BaseResponse } from 'src/models/base.response';
import { NameEndpoint } from 'src/endpoints/name.endpoint';
import { DeleteLetterRequest } from 'src/requests/deleteLetterRequest';
import { AddOrUpdateDictionaryRequest } from 'src/requests/addOrUpdateDictionaryRequest';


@Injectable()
export class NameService {
  constructor(private http: HttpClient, private namesEndpoint: NameEndpoint)
  {

  }

  getAllNames(){
    return this.namesEndpoint.getNamesEndpointAll<DictionaryModel[]>();
  }

  addOrUpdate(model: AddOrUpdateDictionaryRequest){
    return this.namesEndpoint.getNamesAddOrupdateEndpoint<BaseResponse>(model);
  }

  deleteName(deleteRequest: DeleteLetterRequest){
    return this.namesEndpoint.getNamesDeleteEndpoint<BaseResponse>(deleteRequest);
  }
}