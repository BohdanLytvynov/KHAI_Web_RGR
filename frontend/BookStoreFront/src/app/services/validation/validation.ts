import { ElementRef } from "@angular/core";

export abstract class ValidatorBase
{
    validArray : boolean[] = [];

    Init(count : number)
    {
      this.validArray = Array(count).fill(false); 
    }

ResetValidArray()
{
    this.validArray.fill(false);
}

  CheckValidArray() : boolean
  {
    for(let i = 0; i < this.validArray.length; i++)
      if(!this.validArray[i]) return false;
    return true;  
  }

  enableElement(value : boolean, submit : ElementRef)
  {    
    submit.nativeElement.disabled = !value;
  }

}