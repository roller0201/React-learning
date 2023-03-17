import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable, Subject, throwError } from 'rxjs';
import { mergeMap, switchMap, catchError } from 'rxjs/operators';
import { ConfigurationService } from '../services/configuration.service';

@Injectable()
export class EndpointFactory {
  static readonly apiVersion = "1";

  protected getRequestHeaders(): { headers: HttpHeaders | { [header: string]: string | string[]; } } {
    let headers = new HttpHeaders({
      //'Authorization': 'Bearer ' + this.authService.accessToken,
      'Content-Type': 'application/json',
      'Accept': `application/vnd.iman.v${EndpointFactory.apiVersion}+json, application/json, text/plain, */*`,
      'App-Version': `1`,
      //'X-XSRF-TOKEN': `${this.cookieService.get('XSRF-TOKEN')}`,
    });
    return { headers: headers };
  }

  protected getRequestHeadersWithParams(): HttpHeaders {
    let headers = new HttpHeaders({
      //'Authorization': 'Bearer ' + this.authService.accessToken,
      'Content-Type': 'application/json',
      'Accept': `application/vnd.iman.v${EndpointFactory.apiVersion}+json, application/json, text/plain, */*`,
      'App-Version': `1`,
      //'X-XSRF-TOKEN': `${this.cookieService.get('XSRF-TOKEN')}`,
    });
    return headers;
  }

  protected objToSearchParams(obj): HttpParams{
    let params: HttpParams = new HttpParams();
    for (var key in obj) {
        if (obj.hasOwnProperty(key))
            params.append(key, obj[key]);
    }
    return params;
 }
  
  protected handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
}
