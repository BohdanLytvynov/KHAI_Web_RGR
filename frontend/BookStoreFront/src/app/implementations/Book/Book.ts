import { IBook } from "../../interfaces/Book/IBook";
import { Genre } from "../Genre/Genre";

export class Book implements IBook
{
    id: number = 0;
    name: string = '';
    pubYear: number = 0;
    geners: Genre[] = [];    
}