<?php
/**
* Template Name: Filter Page
*
*/

$categories = get_categories( array(
    'orderby' => 'name',
    'order'   => 'ASC'
) );

$loop = new WP_Query( [ 'post_type' => 'product', 'posts_per_page' => -1, 'tax_query' =>[
        [
            'taxonomy' => 'category',
            'terms' => $_COOKIE["cat"],
            'include_children' => false // Remove if you need posts from term 7 child terms
        ],], ] );

get_header();
?>
<style type="text/css">
	.filter-form * {
		padding: 5px;
		margin: 5px;
	}

	.single .post, body.page .page {
    	background-color: unset;
    	border: unset; 
	}

</style>
<div class="wrap">
	<div id="primary" class="content-area">
		<main id="main" class="site-main">
			<header>
				<h1 class="page-title"><?php the_title(); ?></h1>
			</header>
			<div class="entry-content">
				<div class="filter-form">
					<label>Select category:</label>
					<select name="cat">
						<option selected>-none-</option>
						<?php
							foreach ($categories as $category) {
								echo "<option value=" . $category->term_id . " >" . $category->name . "</option>";
							}
						?>
					</select>
					
					<label>Has discount:</label>
					<input type="checkbox" name="has_discount" value="yes">
					<br>
					<label>Lower price:</label>
					<input type="text" name="lprice" value="0">
					
					<label>Upper price:</label>
					<input type="text" name="uprice" value="10">

					<button>Filter</button>
					
				</div>
			</div>
			<?php do_action ('timesnews_frontend_main_banner_after_hook');

		if ( $loop->have_posts() ) :

			if ( is_home() && ! is_front_page() ) :
				?>
				<header>
					<h1 class="page-title screen-reader-text"><?php single_post_title(); ?></h1>
				</header>
				<?php endif; ?>
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

					$discount = get_post_meta( get_the_ID(), 'discount', true );
					$price = get_post_meta( get_the_ID(), 'price', true );

					$has_discount = $_COOKIE["has_discount"];

					if($has_discount == 'true')
					{
						if($discount == "" || $discount == null || $discount == 0)
							continue;
					}

					$lprice = (int)$_COOKIE["lprice"];

					$uprice = (int)$_COOKIE["uprice"];

					if($lprice <= $price && $uprice>= $price)
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
		</main>
	</div>
</div>

<?php
get_footer();