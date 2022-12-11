import { IGames } from "./games";

export interface IPagination{
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IGames[]
}