import { ModuleWithProviders } from '@angular/core';

export interface IEntry {
    id?: string;
    entry: string;
    mydate: string;
    email: string;
}

export var current: string;

export { }

export interface IOrder {
  id?: string;
  entry: string;
  date: string;
  email: string;
}


export interface IState {
    id: number;
    abbreviation: string;
    name: string;
}

export interface IUser {
    id?: string;
    username: string;
    password: string;
    email: string;
}

export interface IPagedResults<T> {
    totalRecords: number;
    results: T;
}

export interface ICustomerResponse {
    status: boolean;
    customer: IEntry;
}

export interface IUserResponse {
  status: boolean;
  user: IUser;
}
