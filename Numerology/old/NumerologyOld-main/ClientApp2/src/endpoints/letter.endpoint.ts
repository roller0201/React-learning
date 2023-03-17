import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfigurationService } from '../services/configuration.service'
import { EndpointFactory } from 'src/environments/endpoint.factory';
import { GetNamesRequest } from 'src/requests/getNamesRequest';


@Injectable()
export class LetterEndpoint extends EndpointFactory
{
  private readonly _lettersUrl: string = "/api/dictionary/lettersAll";
  private readonly _lettersAddOrUpdateUrl: string = "/api/dictionary/addOrUpdateLetter";
  private readonly _lettersDeleteUrl: string = "/api/dictionary/letters/delete";

  get lettersUrl() { return this.configuration.baseUrl + this._lettersUrl; }
  get lettersAddOrUpdateUrl() { return this.configuration.baseUrl + this._lettersAddOrUpdateUrl; }
  get lettersDeleteUrl() { return this.configuration.baseUrl + this._lettersDeleteUrl; }

  constructor(private http: HttpClient, private configuration: ConfigurationService, private injector: Injector)
  {
    super();
  }

  getLettersEndpoint<T>(): Observable<T> {
    let endpoint = this.lettersUrl;
    return this.http.get<T>(endpoint, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getLettersAddOrupdateEndpoint<T>(data: any):Observable<T>{
    let endpoint = this.lettersAddOrUpdateUrl;
    return this.http.post<T>(endpoint, JSON.stringify(data), this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }

  getLettersDeleteEndpoint<T>(data: any):Observable<T>{
    let endpoint = this.lettersDeleteUrl;
    return this.http.post<T>(endpoint, JSON.stringify(data), this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error);
      })
    );
  }
}