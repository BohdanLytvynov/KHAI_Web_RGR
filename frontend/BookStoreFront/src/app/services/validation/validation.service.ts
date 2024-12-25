import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {
  
  constructor() { }

  Name_textField : RegExp = /([A-Z]{1}[a-z]{0,}[^0-9]{0,}\s{0,})/;  
  sequence : RegExp = new RegExp(/[A-Z]{0,}[a-z,][^\s]{0,}/g); 
  number : RegExp = /([\d]{4,})/;

  ValidateText(value: string) : boolean
  {
    if(!this.ValidateTextNotEmpty(value))
      {
        return false;
      }

    let res = this.Name_textField.test(value);

    console.log(res);

    return res;
  }

  ValidateTextNotEmpty(value : string)
  {
    return value.length > 0;
  }  

  ValidateDate(value : string)
  {
    if(!this.ValidateTextNotEmpty(value))
    {
      return false;
    }

    let arr : string[] = value.split('-');

    if(arr.length < 3 || arr.length > 3)
      return false;

    let year : number = 0;
    let month : number = 0;
    let day : number = 0;

    try 
    {
      year = Number(arr[0]);
      month = Number(arr[1]);
      day = Number(arr[2]);
    }
    catch(e)
    {
       return false;
    }    
    return arr[0].length >= 4 && 
    arr[1].length == 2 && 
    arr[2].length == 2 &&
    year > 0 && month > 0 && month <= 12 &&
    day > 0 && day <= 31;
  }

  ValidateNumber(value : string) : boolean
  {
    if(!this.ValidateTextNotEmpty(value))
      return false;
    
    let n = Number(value);
    return !Number.isNaN(n);
  }

  VaidateSequence(value : string) : boolean
  {
    if(!this.ValidateTextNotEmpty(value))
      return false;    
    return this.sequence.test(value);
  }

}
