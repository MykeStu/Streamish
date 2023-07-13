const baseUrl = "/api/UserProfile";
export const getUserVideos = (userId) => {
    return fetch(`${baseUrl}/GetUserProfileWithVideos/${userId}`)
        .then(res => res.json())
}