import { IReqError } from "../../interfaces/ReqError/IReqError";

export class ReqError implements IReqError
{
    error: string = '';
    action: string = '';
    route: string = ''
}