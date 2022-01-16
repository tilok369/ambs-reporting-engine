import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportConfigurationAddComponent } from './report-configuration-add.component';

describe('ReportConfigurationAddComponent', () => {
  let component: ReportConfigurationAddComponent;
  let fixture: ComponentFixture<ReportConfigurationAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportConfigurationAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportConfigurationAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
