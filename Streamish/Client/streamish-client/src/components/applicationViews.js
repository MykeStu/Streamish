import React from "react";
import { Routes, Route } from "react-router-dom";
import VideoList from "./videoList";
import VideoForm from "./videoForm";
import UserVideos from "./userVideos";

const ApplicationViews = () => {
    return (
        <Routes>
            <Route path="/" >
                <Route index element={<VideoList />} />
                <Route path="videos">
                    <Route index element={<VideoList />} />
                    <Route path="add" element={<VideoForm />} />
                    <Route path=":id" element={<p>TODO: Make Video Details component</p>} />
                </Route>
                <Route path="users/:id" element={<UserVideos />} />
            </Route>
            <Route path="*" element={<p>Whoops, nothing here...</p>} />
        </Routes>
    );
};

export default ApplicationViews;