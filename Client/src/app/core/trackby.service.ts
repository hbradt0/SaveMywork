import { Injectable } from '@angular/core';

import { IEntry } from '../shared/interfaces';

@Injectable({
  providedIn: 'root'
})
export class TrackByService {
  
  customer(index: number, customer: IEntry) {
    return customer.id;
  }
  
}
