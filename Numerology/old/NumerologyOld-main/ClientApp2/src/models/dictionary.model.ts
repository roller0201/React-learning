export class DictionaryModel {
  public id: number;
  public name: string;
  public value: string;
  public vowel: boolean;

  constructor(id?: number, name?: string, value?: string, vowel?: boolean) {
    this.id = id;
    this.name = name;
    this.value = value;
    this.vowel = vowel; 
  }
}