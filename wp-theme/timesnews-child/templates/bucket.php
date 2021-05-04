<?php
/**
* Template Name: bucket Page
*
*/

$process_order = $_GET["process_order"];

if($process_order==true && isset($_COOKIE["products"]) )
{
	process_order();
	header("Refresh:0");
	die("order is processed");
}

$categories = get_categories( array(
    'orderby' => 'name',
    'order'   => 'ASC'
) );

get_header();
//var_dump($_COOKIE['products']);
$args = array(
    'post_type' => array( 'product' ),
    'orderby' => 'ASC',
    'post__in' => array_unique(explode(",",$_COOKIE['products']))
);

$loop = new WP_Query( $args );
get_header();
$post_pagination = get_theme_mod('post-pagination','numeric');
?>
<style type="text/css">
.single .post, body.page .page {
    background-color: unset;
    border: unset; 
}

</style>
<div class="wrap">
	<div id="primary" class="content-area">
		<main id="main" class="site-main">

			<?php do_action ('timesnews_frontend_main_banner_after_hook');

		if ( $loop->have_posts() ) :

			if ( is_home() && ! is_front_page() ) :
				?>
				<header>
					<h1 class="page-title screen-reader-text"><?php single_post_title(); ?></h1>
					<br>
				</header>
				<?php endif; ?>

<form>
  <input type="hidden" name="process_order" value="true">

  <label for="name">First name:</label><br>
  <input type="text" id="name" name="name" value=""><br>
  
  <label for="second_name">Last name:</label><br>
  <input type="text" id="second_name" name="second_name" value=""><br>
  
  <label for="phone">Phone:</label><br>
  <input type="tel" id="phone" name="phone"><br>
  
  <label for="address">Address:</label><br>
  <input type="text" id="address" name="address"><br>
  
  <br>
  
</form> 
<button class="order-now">Submit</button>
<br>
			
			<div class="posts-holder">

				<?php
				/* Start the Loop */
				while ( $loop->have_posts() ) :
					$loop->the_post();
					/*
					 * Include the Post-Type-specific template for the content.
					 * If you want to override this in a child theme, then include a file
					 * called content-___.php (where ___ is the Post Type name) and that will be used instead.
					 */
					get_template_part( 'template-parts/content' );
				endwhile;

				if($post_pagination =='default'){
					the_posts_navigation();
				} else {
					the_posts_pagination();
				} ?>
			</div><!-- .posts-holder -->

			<?php else :

				get_template_part( 'template-parts/content', 'none' );

			endif;
			?>
		</main><!-- #main -->
	</div><!-- #primary -->

<?php
	get_sidebar();
 ?>
</div><!-- .wrap -->
<?php
get_footer();