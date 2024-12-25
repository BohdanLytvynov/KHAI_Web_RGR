import { HttpErrorResponse } from "@angular/common/http";
import { IErrorHandler } from "../../interfaces/ErrorHandler/IErrorHandler";
import { IReqError } from "../../interfaces/ReqError/IReqError";

export class ErrorHandler implements IErrorHandler
{
  Handle(err : HttpErrorResponse, actionName : string, route : string): IReqError {
    let r;
      try {
         r = (((err as HttpErrorResponse).error as Array<string>)[0] as any).message; 
      } catch (error) {
        r = 'Unknown error occured!'    
      }

      return { action : actionName, error : r, route : route }
  }
  
}