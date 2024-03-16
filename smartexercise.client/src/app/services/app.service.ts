// app.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  constructor(private http: HttpClient) { }

  getAddress(address: string) {
    return this.http.get(`https://maps.googleapis.com/maps/api/geocode/json?address=${address}&key=YOUR_API_KEY`);
  }
}
