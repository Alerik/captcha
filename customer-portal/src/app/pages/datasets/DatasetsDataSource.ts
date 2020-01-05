import {Dataset} from '../dataset';
import {DataSource, CollectionViewer} from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from 'rxjs';
import { DatasetsService } from '../services/datasets.service';
import { catchError, finalize } from 'rxjs/operators';

export class DatasetsDataSource implements DataSource<Dataset>{
    private datasetsSubject = new BehaviorSubject<Dataset[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private datasetsService : DatasetsService){}

    connect(collectionViewer: CollectionViewer) : Observable<Dataset[]>{
        return this.datasetsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer) : void{
        this.datasetsSubject.complete();
        this.loadingSubject.complete();
    }

    loadDatasets(){
        this.loadingSubject.next(true);
        this.datasetsService.getAll().pipe(
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false))
        )
        .subscribe(datasets => this.datasetsSubject.next(datasets));
    }
}