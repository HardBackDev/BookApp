import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { enviroment } from '../enviroments/environment';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  url = `${enviroment.apiUrl}/file`

  constructor(private http: HttpClient) { }

  public uploadFile = (formData) => {
    return this.http.post(`${this.url}/upload`, formData, {reportProgress: true, observe: 'events'})
  }

  public download(fileUrl: string) {
    return this.http.get(`${this.url}/download?fileUrl=${fileUrl}`, {
      reportProgress: true,
      observe: 'events',
      responseType: 'blob'
    }); 
  }
}
