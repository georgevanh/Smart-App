import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from '../../models/customer.model';
import { CustomerService } from '../../services/customer.service'

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customers: Customer[] = [];
  constructor(private router: Router, private customerService: CustomerService) { }

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe(customers => {
      this.customers = customers;
    })
  }

  viewDetails(id: number) {
    this.router.navigate(['/customers', id]);
  }
}
