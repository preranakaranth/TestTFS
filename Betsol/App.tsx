/// <reference path="../../../typings/index.d.ts" />
import * as React from 'react';

const ParticlesLibrary = require( "react-particles-js" );
const Particles = ParticlesLibrary.default;

const ReactMarkdown = require( "react-markdown" );

export interface AppProps{
	children ?: any;
}

export interface AppState{
	result ?: string;
	markdown ?: string;
	locked: boolean;
}

let intervals: any[] = [];

class App extends React.Component<AppProps, AppState>{
	constructor( props: any ){
		super(props);
		this.state = {
			result: "",
			markdown: "### markdown di test  #### test  **another test**",
			locked: false
		};
		this.fetchTime = this.fetchTime.bind( this );
		this.fetchContent = this.fetchContent.bind( this );
		this.onMarkdownContainerScroll = this.onMarkdownContainerScroll.bind( this );
		
	}

	markdownContainer: HTMLElement;

	componentWillMount(){
		intervals.push( setInterval( this.fetchTime, 1000 ) );
		intervals.push( setInterval( this.fetchContent, 2000 ) );
		this.fetchContent();
	}

	componentWillUnmount(){
		intervals.forEach( ( interval ) => {
			clearInterval( interval );
		});
		intervals = [];
	}
	
	fetchTime(){
		fetch( "/home/time" )
			.then( ( response ) => {
				return response.json();
			})
			.then( ( response: Response & { result: string } ) => {
				this.setState({
					result: response.result,
					markdown: this.state.markdown,
					locked: this.state.locked
				});
			});
	}

	fetchContent(){
		return fetch( "/home/content" )
			.then( ( response ) => {
				return response.json();
			})
			.then( ( response: Response & { content: string } ) => {
				return this.setState({
					result: this.state.result,
					markdown: response.content,
					locked: this.state.locked
				});
			})
			.then( () => {
				if( this.state.locked ){
					if( this.markdownContainer ){
						this.markdownContainer.scrollTop = this.markdownContainer.scrollHeight;
					}
				}
			});
	}

	onMarkdownContainerScroll( event: React.FormEvent ){
		this.setState({
			...this.state,
			locked: false
		});
		event.currentTarget
		let target: any = event.target;
		if( target.scrollHeight - target.scrollTop == target.clientHeight ){
			this.setState({
				...this.state,
				locked: true
			});
		}
	}

	render(){
		return (
			<div className="particles-container">
				<Particles
					params={{
						particles: {
							number: {
								value: 40
							},
							line_linked: {
								opacity: 0.8,
								color: '#32CD32',
								shadow: {
									enable: false,
									color: "white",
									blur: 10
								}
							},
							color: {
								value: "#42d4f4"
							},
							move: {
								speed: 1
							}
						},
						interactivity: {
							events: {
								onclick: {
									enable: true,
									mode: "repulse"
								}
							}
						},
						modes: {
							repulse: {
								distance: 50,
								duration: 4
							}
						}
					}}
					style={{
						background: "black",
						position: "fixed",
						top: 0,
						left: 0,
						width: "100%",
						height: "100%",
						zIndex: 1
					}}/>
				<div
					style={{
						display: "inline-block",
						textAlign: "center",
						position: "fixed",
						left: "20px",
						top: "20px",
						color: "#FFF",
						font: "100 20px 'Helvetica Neue', sans-serif",
						zIndex: 2,
						textShadow: "0px 0px 15px black"
					}}>
					{this.state.result}
					<a href="https://github.com/Wufe/dummy-dotnet-core" target="_blank">Source Code</a>
				</div>
				<div
					ref={f => this.markdownContainer = f}
					onScroll={this.onMarkdownContainerScroll}
					style={{
						display: "block",
						position: "fixed",
						left: "20px",
						top: "50px",
						right: "20px",
						bottom: "20px",
						color: "#E1E1E1",
						font: "100 16px 'Helvetica Neue', sans-serif",
						zIndex: 3,
						opacity: 0.8,
						textAlign: "left",
						overflow: "auto"
					}}>
					<ReactMarkdown source={this.state.markdown} />
				</div>
			</div>
		);
	}
}

export default App;