import { IUser } from "../../interfaces/User/IUser";

export class User implements IUser
{
    nickname: string = '';
    password: string = '';
    name: string = '';
    surename: string = '';
    birthday: string = '';
    address: string = '';
    
}