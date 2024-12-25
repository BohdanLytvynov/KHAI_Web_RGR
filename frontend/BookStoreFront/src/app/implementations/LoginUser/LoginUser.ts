import { ILoginUser } from "../../interfaces/LoginUser/ILoginUser";

export class LoginUser implements ILoginUser
{
    nickname: string = '';
    password: string = '';
    
}