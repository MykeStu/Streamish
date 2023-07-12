import React from "react";
import { useState, useEffect } from "react";
import { searchAllVideos, getAllVideos } from "../modules/videoManager";
import Video from "./video";
import VideoForm from "./videoForm";

const VideoList = () => {
    const [videos, setVideos] = useState([]);
    const [searchParams, setSearchParams] = useState();

    const getVideos = () => {
        getAllVideos().then(data => setVideos(data));
    }

    const searchVideos = () => {
        searchAllVideos(searchParams).then(data => setVideos(data));
    }

    useEffect(
        () => {
            searchParams != null ?
                searchVideos()
                : getVideos();
        }, [searchParams]
    );

    return (
        <div>
            <div>Search</div>
            <input type="text" onChange={
                event => setSearchParams(event.target.value)
            } />
            <div className="videoFormContainer">
                <VideoForm />
            </div>
            <div className="container">
                <div className="row justify-content-center">
                    {videos.map((video) => (
                        <Video video={video} key={video.id} />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default VideoList;