
import { Injectable } from '@angular/core';
import { NumerologyEndpoint } from 'src/endpoints/numerology.endpoint';
import { NumerologyNameModel } from 'src/models/numerologyName.model';
import { NumerologyPortrait } from 'src/models/numerologyPortrait.model';
import { BaseResponse } from 'src/models/base.response';

@Injectable()
export class NumerologyService {
  constructor(private portraitEndpoint: NumerologyEndpoint)
  {

  }

  getNumerologyName(name: string){
    return this.portraitEndpoint.getNamesEndpoint<NumerologyNameModel>(name);
  }
  
  getNumerologyNameWhole(baseNames: string, addedNames: string){
    return this.portraitEndpoint.getNamesWholeEndpoint<NumerologyNameModel>(baseNames, addedNames);
  }

  deleteNumerologyPortrait(id: number){
    return this.portraitEndpoint.deleteNumerologyPortraitEndpoint<any>(id);
  }

  getNumerologyBirthDate(birthDate: string){
    return this.portraitEndpoint.getNumerologyBirthDate<any>(birthDate);
  }

  getNumerologyBirthDateTree(birthDate: string){
    return this.portraitEndpoint.getNumerologyBirthDateTree<any>(birthDate);
  }

  getClientPortraits(id: number){
    return this.portraitEndpoint.getClientPortraits<NumerologyPortrait[]>(id);
  }

  addNewPortrait(req: NumerologyPortrait){
    return this.portraitEndpoint.addNewPortraitEndpoint<BaseResponse[]>(req);
  }

  printPortrait(req: any){
    return this.portraitEndpoint.printPortraitEndpoint<any>(req);
  }

  getPrintPortrait(path: string){
    return this.portraitEndpoint.getPrintPortraitEndpoint<any>(path);
  }
}