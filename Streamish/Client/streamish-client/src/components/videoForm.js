import React from "react";
import { useState } from "react";
import { addVideo } from "../modules/videoManager"
import { useNavigate } from "react-router";

const VideoForm = () => {
    const [newVideo, updateNewVideo] = useState({
        Title: "",
        Description: "",
        Url: ""
    });

    const navigate = useNavigate();

    const newVideoButton = (event) => {
        event.preventDefault();
        let nV = {
            Title: newVideo.Title,
            Description: newVideo.Description,
            DateCreated: new Date(),
            Url: newVideo.Url,
            UserProfileId: Math.floor(Math.random(1, 5))
        };

        addVideo(nV).then((p) => {
            navigate("/");
        });
    }

    return (
        <div>
            <center><h4>Add New Video</h4></center>
            <div style={{ margin: "1rem" }}>
                <div>
                    <div>Title</div>
                    <input
                        required autoFocus
                        type="text"
                        placeholder="Enter Title Here"
                        value={newVideo.Title}
                        onChange={
                            (event) => {
                                const copy = { ...newVideo }
                                copy.Title = event.target.value
                                updateNewVideo(copy);
                            }
                        } />
                </div>
                <div>
                    <div>Description</div>
                    <input
                        required autoFocus
                        type="text-area"
                        placeholder="Enter Description Here"
                        value={newVideo.Description}
                        onChange={
                            (event) => {
                                const copy = { ...newVideo }
                                copy.Description = event.target.value
                                updateNewVideo(copy);
                            }
                        } />
                </div>
                <div>
                    <div>Url</div>
                    <input
                        required autoFocus
                        type="text"
                        placeholder="Youtube Url Here"
                        value={newVideo.Url}
                        onChange={
                            (event) => {
                                const copy = { ...newVideo }
                                copy.Url = event.target.value
                                updateNewVideo(copy);
                            }
                        } />
                </div>
            </div>
            <button onClick={event => newVideoButton(event)}>Add New Video</button>
        </div>
    );
};
export default VideoForm;