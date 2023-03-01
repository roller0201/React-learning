export class NumerologyNameModel {
  public upRow: string;
  public downRow: string;
  public upRowMainWhole: string;
  public downRowMainWhole: string;
  public upRowMain: string;
  public downRowMain: string;
  public one: string;
  public two: string;
  public three: string;
  public four: string;
  public five: string;
  public six: string;
  public seven: string;
  public eight: string;
  public nine: string;

  constructor(jsonObj: any)
  {
    debugger;
    if(jsonObj == null)
    {
      this.upRow = "";
      this.downRow = "";
      this.upRowMainWhole = "";
      this.downRowMainWhole = "";
      this.upRowMain = "";
      this.downRowMain = "";
      this.one = "";
      this.two = "";
      this.three = "";
      this.four = "";
      this.five = "";
      this.six = "";
      this.seven = "";
      this.eight = "";
      this.nine = "";
    }
    else
    {
      this.upRow = jsonObj.upRow;
      this.downRow = jsonObj.downRow;
      this.upRowMainWhole = jsonObj.upRowMainWhole;
      this.downRowMainWhole = jsonObj.downRowMainWhole;
      this.upRowMain = jsonObj.upRowMain;
      this.downRowMain = jsonObj.downRowMain;
      this.one = jsonObj.one;
      this.two = jsonObj.two;
      this.three = jsonObj.three;
      this.four = jsonObj.four;
      this.five = jsonObj.five;
      this.six = jsonObj.six;
      this.seven = jsonObj.seven;
      this.eight = jsonObj.eight;
      this.nine = jsonObj.nine;
    }
  }
}