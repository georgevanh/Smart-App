import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from '../../models/customer.model';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css']
})
export class CustomerDetailsComponent implements OnInit {
  customer: Customer = {
      firstName: '',
      lastName: '',
      email: '',
      mobileNumber: '',
      address: ''
  };
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService
  ) { }

  ngOnInit(): void {
    this.loadCustomer();
  }

  loadCustomer(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.customerService.getCustomerById(id).subscribe(customer => {
      this.customer = customer;
    });
  }


  goBack(): void {
    // Navigate back to the previous page
    this.router.navigate(['/customers']);
  }



}
