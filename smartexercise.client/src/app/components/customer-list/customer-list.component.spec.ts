import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { CustomerListComponent } from './customer-list.component';
import { Customer } from '../../models/customer.model';
import { CustomerService } from '../../services/customer.service';

describe('CustomerListComponent', () => {
  let component: CustomerListComponent;
  let fixture: ComponentFixture<CustomerListComponent>;
  let customerService: jasmine.SpyObj<CustomerService>;

  beforeEach(async () => {
    const customerServiceSpy = jasmine.createSpyObj('CustomerService', ['getCustomers']);

    await TestBed.configureTestingModule({
      declarations: [CustomerListComponent],
      imports: [RouterTestingModule],
      providers: [{ provide: CustomerService, useValue: customerServiceSpy }]
    }).compileComponents();

    customerService = TestBed.inject(CustomerService) as jasmine.SpyObj<CustomerService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch customers on initialization', () => {
    const mockCustomers: Customer[] = [
      { id: 1, firstName: 'John', lastName: 'Doe', email: 'john@example.com', mobileNumber: '1234567890', address: '123 Street' },
      { id: 2, firstName: 'Jane', lastName: 'Doe', email: 'jane@example.com', mobileNumber: '9876543210', address: '456 Avenue' }
    ];
    customerService.getCustomers.and.returnValue(of(mockCustomers));

    fixture.detectChanges();

    expect(component.customers).toEqual(mockCustomers);
  });

  it('should navigate to customer details page when viewDetails is called', () => {
    const id = 1;
    const navigateSpy = spyOn(component.router, 'navigate');

    component.viewDetails(id);

    expect(navigateSpy).toHaveBeenCalledWith(['/customers', id]);
  });
});
