import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ResultModel} from "../models/result.model";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) {
  }

  post<T>(apiUrl: string, body: any, callBack: (res: ResultModel<T>) => void, errCallBack?: (err: HttpErrorResponse) => void) {
    this.http.post<ResultModel<T>>(`${apiUrl}`, body)
      .subscribe({
        next: (res => {
          callBack(res);
        }),
        error: (err: HttpErrorResponse) => {
          if (errCallBack !== undefined) {
            errCallBack(err);
          }
        }
      })
  }
}
