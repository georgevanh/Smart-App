// app.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class AppService {
  private readonly GOOGLE_MAPS_API_URL = environment.GOOGLE_MAPS_API_URL;
  private readonly GOOGLE_MAPS_API_KEY = environment.GOOGLE_MAPS_API_KEY;
  constructor(private http: HttpClient) { }

  getAddress(address: string) {
    const url = `${this.GOOGLE_MAPS_API_URL}?address=${address}&key=${this.GOOGLE_MAPS_API_KEY}`;
    return this.http.get(url);    
  }
}
