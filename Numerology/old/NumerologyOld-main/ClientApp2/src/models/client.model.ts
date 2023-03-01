import * as moment from 'moment';

export class ClientModel {
  public id: number;
  public name: string;
  public surname: string;
  public birthDate: string;//moment.Moment;
  public active: boolean;
  public telephone: string;
  public email: string;
  public skype: string;
  public entryDate: string;//moment.Moment;
  public note: string;

  constructor(id: number,
              name: string,
              surname: string,
              birthDate: string,
              active: boolean,
              telephone: string,
              email: string,
              skype: string,
              entryDate: string,
              note: string)
  {
    this.id = id;
    this.name = name;
    this.surname = surname;
    if(birthDate != "")
      this.birthDate = birthDate;//moment(birthDate, "YYYY-MM-DD");
    else
      this.birthDate = "";//moment();
    this.active = active;
    this.telephone = telephone;
    this.email = email;
    this.skype = skype;
    if(entryDate != "")
      this.entryDate = entryDate;//moment(entryDate, "YYYY-MM-DD");
    else
      this.entryDate = ""//moment();
    this.note = note;
  }
}