const baseUrl = '/api/video';

export const getAllVideos = () => {
    return fetch(`${baseUrl}`)
        .then((res) => res.json())
};
export const searchAllVideos = (searchParams) => {
    return fetch(`${baseUrl}/search?q=${searchParams}`)
        .then((res) => res.json())
}
export const addVideo = (video) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(video),
    });
};
export const getVideo = (id) => {
    return fetch(`${baseUrl}/GetByIdWithComments/${id}`).then((res) => res.json());
};