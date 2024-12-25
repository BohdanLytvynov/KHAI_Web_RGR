import { Genre } from "../../implementations/Genre/Genre";

export interface IBook {
    id: number;
    name: string;
    pubYear: number;
    geners: Genre[];
  }