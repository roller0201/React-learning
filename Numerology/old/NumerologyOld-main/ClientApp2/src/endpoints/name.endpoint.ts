import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfigurationService } from '../services/configuration.service'
import { EndpointFactory } from 'src/environments/endpoint.factory';
import { GetNamesRequest } from 'src/requests/getNamesRequest';

@Injectable()
export class NameEndpoint extends EndpointFactory {
  private readonly _namesUrl: string = "/api/dictionary/names";
  private readonly _namesAllUrl: string = "/api/dictionary/namesAll";
  private readonly _namesAddOrUpdateUrl: string = "/api/dictionary/addOrUpdateName";
  private readonly _namesDeleteUrl: string = "/api/dictionary/names/delete";

  get namesUrl() { return this.configuration.baseUrl + this._namesUrl; }
  get namesAllUrl() { return this.configuration.baseUrl + this._namesAllUrl; }
  get namesAddOrUpdateUrl() { return this.configuration.baseUrl + this._namesAddOrUpdateUrl; }
  get namesDeleteUrl() { return this.configuration.baseUrl + this._namesDeleteUrl; }


  constructor(private http: HttpClient, private configuration: ConfigurationService, private injector: Injector)
  {
    super();
  }

  /// We should use this
  getNamesEndpoint<T>(request: GetNamesRequest): Observable<T> {
    let endpoint = this.namesUrl;
    //let params2: HttpParams = this.objToSearchParams(request);
    //let headers2: HttpHeaders = this.getRequestHeadersWithParams();
    //debugger;
    /*return this.http.get<T>(endpoint, { headers: headers2, params: params2}).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );*/
    let endpoint2 = endpoint + '/0/15'; // Change this
    return this.http.get<T>(endpoint2, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getNamesEndpointAll<T>(): Observable<T> {
    let endpoint = this.namesAllUrl;
    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }


  getNamesAddOrupdateEndpoint<T>(data: any):Observable<T>{
    let endpoint = this.namesAddOrUpdateUrl;
    return this.http.post<T>(endpoint, JSON.stringify(data), this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getNamesDeleteEndpoint<T>(data: any):Observable<T>{
    let endpoint = this.namesDeleteUrl;
    return this.http.post<T>(endpoint, JSON.stringify(data), this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }
}