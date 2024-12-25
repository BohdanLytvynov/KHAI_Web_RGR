import { HttpErrorResponse } from "@angular/common/http";
import { IReqError } from "../ReqError/IReqError";

export declare interface IErrorHandler
{
  Handle(err : HttpErrorResponse, actionName : string, route : string) : IReqError;
}