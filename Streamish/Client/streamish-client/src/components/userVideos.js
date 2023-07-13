import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Video from "./video.js";
import { getUserVideos } from "../modules/userManager.js";
const UserVideos = () => {
    const [userProfile, setUser] = useState({});
    const { id } = useParams();

    useEffect(() => {
        getUserVideos(id)
            .then((data) => setUser(data))
    }, [])

    return <div>
        <p><h2>{userProfile.name}</h2></p>
        {
            userProfile.videos ?
                userProfile.videos.map(v => <Video video={v} />)
                : "No videos"
        }
    </div>
}
export default UserVideos;