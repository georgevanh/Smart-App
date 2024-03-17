import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { CustomerDetailsComponent } from './customer-details.component';
import { Customer } from '../../models/customer.model';
import { CustomerService } from '../../services/customer.service';

describe('CustomerDetailsComponent', () => {
  let component: CustomerDetailsComponent;
  let fixture: ComponentFixture<CustomerDetailsComponent>;
  let activatedRoute: ActivatedRoute;
  let router: Router;
  let customerService: jasmine.SpyObj<CustomerService>;

  beforeEach(async () => {
    const customerServiceSpy = jasmine.createSpyObj('CustomerService', ['getCustomerById']);

    await TestBed.configureTestingModule({
      declarations: [CustomerDetailsComponent],
      providers: [
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } },
        { provide: Router, useValue: { navigate: jasmine.createSpy('navigate') } },
        { provide: CustomerService, useValue: customerServiceSpy }
      ]
    }).compileComponents();

    activatedRoute = TestBed.inject(ActivatedRoute);
    router = TestBed.inject(Router);
    customerService = TestBed.inject(CustomerService) as jasmine.SpyObj<CustomerService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerDetailsComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load customer details on initialization', () => {
    const mockCustomer: Customer = {
      id: 1,
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      mobileNumber: '1234567890',
      address: '123 Street'
    };
    customerService.getCustomerById.and.returnValue(of(mockCustomer));

    fixture.detectChanges();

    expect(component.customer).toEqual(mockCustomer);
  });

  it('should navigate back to customers page when goBack is called', () => {
    component.goBack();

    expect(router.navigate).toHaveBeenCalledWith(['/customers']);
  });
});
