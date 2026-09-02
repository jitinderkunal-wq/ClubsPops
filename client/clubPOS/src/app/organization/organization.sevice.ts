import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { EditUser } from "app/shared/models/editUser";
import { Organization } from "app/shared/models/organization";
import { environment } from "environments/environment.development";

@Injectable({
    providedIn: 'root'
})

export class OrganizationService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient, private router: Router) { }

    // Organization Services
    getOrganizations() {
        return this.http.get<Organization[]>(this.baseUrl + 'Organization');
    }

    getOrganizationById(id: number) {
        return this.http.get<Organization>(this.baseUrl + 'Organization/' + id);
    }

    uploadLogo(file: FormData) {
        return this.http.post<any>(this.baseUrl + 'Organization/uploadLogo', file);
    }

    createOrganization(values: any) {
        return this.http.post<Organization>(this.baseUrl + 'Organization', values);
    }

    editOrganization(values: any) {
        return this.http.patch<Organization>(this.baseUrl + 'Organization', values);
    }

    deleteOrganization(id: number) {
        return this.http.delete(this.baseUrl + 'Organization/' + id);
    }

    // Admin Services
    getAdmins(id: number) {
        return this.http.get<EditUser[]>(this.baseUrl + 'Admin/' + id);
    }

}  