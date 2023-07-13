import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";


const Video = ({ video }) => {
    return (
        <Card className="column">
            <div>
                {video.userProfile ?
                    <Link to={`/users/${video.userProfile.id}`} className="text-left px-2">Posted by: {video.userProfile.name}</Link>
                    : ""
                }
            </div>
            <CardBody>
                <iframe className="video"
                    src={video.url}
                    title="YouTube video player"
                    frameBorder="0"
                    allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                    allowFullScreen />

                <Link to={`/videos/${video.id}`}>
                    <p><strong>{video.title}</strong></p>
                </Link>
                <p>{video.description}</p>
                {
                    video.comments ?
                        video.comments.map(c => { return <li>{c.message}</li> })
                        : ""
                }
            </CardBody>
        </Card>
    );
};

export default Video;