import { IAuthor } from "../../interfaces/Author/IAuthor";

export class Author implements IAuthor
{
    id: number = -1;
    name: string = '';
    surename: string = '';
    birthDate: string = '';
    
}