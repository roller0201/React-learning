import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfigurationService } from '../services/configuration.service'
import { EndpointFactory } from 'src/environments/endpoint.factory';

@Injectable()
export class ClientEndpoint extends EndpointFactory{
  private readonly _clientsUrl: string = "/api/client/clients";
  private readonly _clientsAllUrl: string = "/api/client/clientsAll";
  private readonly _clientAddOrUpdateUrl: string = "/api/client/addOrUpdateClient";

  get clientsUrl() { return this.configuration.baseUrl + this._clientsUrl; }
  get clientsAddOrUpdateUrl() { return this.configuration.baseUrl + this._clientAddOrUpdateUrl; }
  get clientsAllUrl() { return this.configuration.baseUrl + this._clientsAllUrl; }

  constructor(private http: HttpClient, private configuration: ConfigurationService, private injector: Injector)
  {
    super();
  }

  getClientsEndpoint<T>(page: number, countOnPage: number): Observable<T> {
    let endpoint = this.clientsUrl + '/' + page.toString() + '/' + countOnPage.toString();
    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getAllClientsEndpoint<T>(): Observable<T> {
    let endpoint = this.clientsAllUrl;
    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getClientsAddOrupdateEndpoint<T>(data: any):Observable<T>{
    let endpoint = this.clientsAddOrUpdateUrl;
    return this.http.post<T>(endpoint, JSON.stringify(data), this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

}
