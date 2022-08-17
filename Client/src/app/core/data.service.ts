import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError,  } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { IEntry, IState, IPagedResults, ICustomerResponse, IUser, IUserResponse } from '../shared/interfaces';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class DataService {
    baseUrl = environment.apiUrl;
  baseCustomersUrl = this.baseUrl + 'customers';
  baseUsersUrl = this.baseUrl + 'users';
    baseStatesUrl = this.baseUrl + 'states'

    constructor(private http: HttpClient) { }
    
  getCustomers(): Observable<IEntry[]> {
    return this.http.get<IEntry[]>(this.baseCustomersUrl)
            .pipe(
                   map(customers => {
                       return customers;
                   }),
                   catchError(this.handleError)
                );
    }

  getCustomersPage(page: number, pageSize: number): Observable<IPagedResults<IEntry[]>> {
      return this.http.get<IEntry[]>(`${this.baseCustomersUrl}/page/${page}/${pageSize}`, {observe: 'response'})
            .pipe(            
                map((res) => {
                    //Need to observe response in order to get to this header (see {observe: 'response'} above)
                    const xInlineCount = res.headers.get('X-InlineCount');
                    const totalRecords = Number(xInlineCount);
                  let customers = res.body as IEntry[];
                    return {
                        results: customers,
                        totalRecords: totalRecords
                    };
                }),
                catchError(this.handleError)
            );
    }
    
  getCustomer(id: string): Observable<IEntry> {
    return this.http.get<IEntry>(this.baseCustomersUrl + '/' + id)
            .pipe(
                catchError(this.handleError)
            );
  }

  getUser(email: string): Observable<IUser> {
    return this.http.get<IUser>(this.baseUsersUrl + '/' + email)
      .pipe(
        catchError(this.handleError)
      );
  }

  insertCustomer(customer: IEntry): Observable<IEntry> {
        return this.http.post<ICustomerResponse>(this.baseCustomersUrl, customer)
            .pipe(                   
                map((data) => {
                       console.log('insertCustomer status: ' + data.status);
                       return data.customer;
                   }),
                catchError(this.handleError)
            );
  }

  insertUser(user: IUser): Observable<IUser> {
    return this.http.post<IUserResponse>(this.baseUsersUrl, user)
      .pipe(
        map((data) => {
          console.log('insertUser status: ' + data.status);
          return data.user;
        }),
        catchError(this.handleError)
      );
  }
   
  updateCustomer(customer: IEntry): Observable<IEntry> {
        return this.http.put<ICustomerResponse>(this.baseCustomersUrl + '/' + customer.id, customer) 
            .pipe(
                map((data) => {
                       console.log('updateCustomer status: ' + data.status);
                       return data.customer;
                   }),
                catchError(this.handleError)
            );
    }

    deleteCustomer(id: string) : Observable<boolean> {
        return this.http.delete<boolean>(this.baseCustomersUrl + '/' + id)
            .pipe(
                catchError(this.handleError)
            );
    }
   
    getStates(): Observable<IState[]> {
        return this.http.get<IState[]>(this.baseStatesUrl)
            .pipe(
                catchError(this.handleError)
            )

    }
    
    private handleError(error: HttpErrorResponse) {
        console.error('server error:', error); 
        if (error.error instanceof Error) {
          let errMessage = error.error.message;
          return throwError(() => new Error(errMessage));
          // Use the following instead if using lite-server
          //return Observable.throw(err.text() || 'backend server error');
        }
        return throwError(() => new Error(error.message || 'Node.js server error'));
    }

}
