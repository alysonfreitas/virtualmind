import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  public getHttpOptions() {
    return {
      headers: {
        'Content-Type': 'application/json'
      }
    };
  }

  public getCurrency(iso: string): Observable<any> {
    const options = this.getHttpOptions();
    return this.http.get(`${environment.apiURL}/currency/${iso}`, options);
  }

  public newTransaction(transaction: any): Observable<any> {
    const options = this.getHttpOptions();
    return this.http.post(`${environment.apiURL}/transaction`, JSON.stringify(transaction), options);
  }
}
