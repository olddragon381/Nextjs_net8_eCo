export interface UserProfile {
    
    email : string,
    fullName: string,
    profile: UpdateProfile
}
export interface UpdateProfile{
    
    nameForOrder: string,
    phone: string,
    address: string
}

