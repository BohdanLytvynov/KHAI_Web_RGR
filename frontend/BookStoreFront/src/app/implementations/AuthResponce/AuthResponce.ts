import { IAuthResponse } from "../../interfaces/AuthResponce/IAuthResponce";

export class AuthResponce implements IAuthResponse
{
    address: string = '';
    birthday: string = '';
    name: string = '';
    nickname: string = '';
    surename: string = '';
    
}