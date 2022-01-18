import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MetaDataService } from 'src/app/services/meta-data.service';

@Component({
  selector: 'app-report-metadata',
  templateUrl: './report-metadata.component.html',
  styleUrls: ['./report-metadata.component.css']
})
export class ReportMetadataComponent implements OnInit {

  public metadatas: any[] = [];
  constructor(private router: Router, private metaDataService: MetaDataService) { }

  ngOnInit(): void {
    this.getMetadatas(1, 10);
  }

  getMetadatas(page, size){
    this.metaDataService.getMetadatas(page, size).subscribe((res: any) => {
      console.log(res);
      this.metadatas = res;
    });
  }

  addMetadata(){
    this.router.navigateByUrl('/report-metadata-add');
  }

  editMetadata(id){
    this.router.navigateByUrl('/report-metadata-edit', {state: {metadataId: id}});
  }

} 

