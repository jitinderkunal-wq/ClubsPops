import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashDropDrawerSkimsComponent } from './cash-drop-drawer-skims.component';

describe('CashDropDrawerSkimsComponent', () => {
  let component: CashDropDrawerSkimsComponent;
  let fixture: ComponentFixture<CashDropDrawerSkimsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CashDropDrawerSkimsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CashDropDrawerSkimsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
